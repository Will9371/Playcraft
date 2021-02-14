// Credit: Mert Kirimgeri
// Source: https://www.youtube.com/watch?v=OMDfr1dzBco

using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

namespace Playcraft.Experimental.DialogGraph
{
    public class GraphSaveUtility
    {
        DialogGraphView _targetGraphView;
        DialogContainer _containerCache;
        
        List<Edge> edges => _targetGraphView.edges.ToList();
        List<DialogNode> nodes => _targetGraphView.nodes.ToList().Cast<DialogNode>().ToList();

        public static GraphSaveUtility GetInstance(DialogGraphView targetGraphView)
        {
            return new GraphSaveUtility { _targetGraphView = targetGraphView };
        }
        
        public void SaveGraph(string fileName)
        {
            if (!edges.Any()) return;
            
            var dialogContainer = ScriptableObject.CreateInstance<DialogContainer>();
            
            var connectedPorts = edges.Where(x => x.input.node != null).ToArray();
            
            for (int i = 0; i < connectedPorts.Length; i++)
            {
                var outputNode = connectedPorts[i].output.node as DialogNode;
                var inputNode = connectedPorts[i].input.node as DialogNode;
                
                dialogContainer.nodeLinks.Add(new NodeLinkData
                {
                   baseNodeGuid = outputNode.guid,
                   portName = connectedPorts[i].output.portName,
                   taretNodeGuid = inputNode.guid
                });
            }
            
            foreach (var dialogNode in nodes.Where(node => !node.entryPoint))
            {
                dialogContainer.dialogNodeData.Add(new DialogNodeData
                {
                    guid = dialogNode.guid,
                    dialogText = dialogNode.dialogText,
                    position = dialogNode.GetPosition().position
                });
            }
            
            // Auto-create Resource folder if it does not exist
            if (!AssetDatabase.IsValidFolder("Assets/Resources"))
                AssetDatabase.CreateFolder("Assets", "Resources");
            
            AssetDatabase.CreateAsset(dialogContainer, $"Assets/Resources/{fileName}.asset");
            AssetDatabase.SaveAssets();
        }
        
        public void LoadGraph(string fileName)
        {
            _containerCache = Resources.Load<DialogContainer>(fileName);
            
            if (_containerCache == null)
            {
                EditorUtility.DisplayDialog("File not found!", "Target dialog graph file does not exist.", "OK");
                return;
            }
            
            ClearGraph();
            CreateNodes();
            ConnectNodes(); 
        }
        
        void ClearGraph()
        {
            // Set entry point's guid back from the save. Discard existing guid.
            nodes.Find(x => x.entryPoint).guid = _containerCache.nodeLinks[0].baseNodeGuid;
            
            foreach (var node in nodes)
            {
                if (node.entryPoint) continue;
                
                // Remove the edges connected to this node
                edges.Where(x => x.input.node == node).ToList().
                    ForEach(edge => _targetGraphView.RemoveElement(edge));
                
                // And then remove the node
                _targetGraphView.RemoveElement(node);
            }
        }
        
        void CreateNodes()
        {
            foreach (var nodeData in _containerCache.dialogNodeData)
            {
                var tempNode = _targetGraphView.CreateDialogNode(nodeData.dialogText);
                tempNode.guid = nodeData.guid;
                _targetGraphView.AddElement(tempNode);
                
                var nodePorts = _containerCache.nodeLinks.Where(x => x.baseNodeGuid == nodeData.guid).ToList();
                nodePorts.ForEach(x => _targetGraphView.AddChoicePort(tempNode, x.portName));
            }
        }
        
        void ConnectNodes()
        {
            for (int i = 0; i < nodes.Count; i++)
            {
                var connections = _containerCache.nodeLinks.Where(x => x.baseNodeGuid == nodes[i].guid).ToList();
                for (int j = 0; j < connections.Count; j++)
                {
                    var targetNodeGuid = connections[j].taretNodeGuid;
                    var targetNode = nodes.First(x => x.guid == targetNodeGuid);
                    LinkNodes(nodes[i].outputContainer[j].Q<Port>(), (Port)targetNode.inputContainer[0]);
                    
                    targetNode.SetPosition(new Rect(_containerCache.dialogNodeData.First(x => 
                        x.guid == targetNodeGuid).position, _targetGraphView.defaultNodeSize));
                }
            }
        }
        
        void LinkNodes(Port output, Port input)
        {
            var edge = new Edge
            {
                output = output,
                input = input
            };
            edge?.input.Connect(edge);
            edge?.output.Connect(edge);
            _targetGraphView.Add(edge);
        }
    }
}
