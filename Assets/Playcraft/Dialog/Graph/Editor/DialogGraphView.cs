// Credit: Mert Kirimgeri
// Source: https://www.youtube.com/watch?v=7KHGH0fPL84

using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

namespace Playcraft.Experimental.DialogGraph
{
    public class DialogGraphView : GraphView
    {
        public readonly Vector2 defaultNodeSize = new Vector2(150, 200);
        
        public DialogGraphView()
        {
            styleSheets.Add(Resources.Load<StyleSheet>("DialogGraph"));
            SetupZoom(ContentZoomer.DefaultMinScale, ContentZoomer.DefaultMaxScale);
        
            this.AddManipulator(new ContentDragger());
            this.AddManipulator(new SelectionDragger());
            this.AddManipulator(new RectangleSelector());
            
            var grid = new GridBackground();
            Insert(0, grid);
            grid.StretchToParentSize();
            
            AddElement(GenerateEntryPointNode());
        }
        
        DialogNode GenerateEntryPointNode()
        {
            var node = new DialogNode
            {
                title = "Start",
                guid = Guid.NewGuid().ToString(),
                dialogText = "",
                entryPoint = true
            };
            
            var outputPort = GeneratePort(node, Direction.Output);
            outputPort.portName = "Next";
            node.outputContainer.Add(outputPort);
            
            //node.capabilities &= ~Capabilities.Movable;
            node.capabilities &= ~Capabilities.Deletable;
            
            node.RefreshExpandedState();
            node.RefreshPorts();
            
            node.SetPosition(new Rect(100, 200, 100, 150));
            return node;
        }
        
        Port GeneratePort(DialogNode node, Direction portDirection, Port.Capacity capacity = Port.Capacity.Single)
        {
            // Last argument is used for transmitting information between nodes (not needed for dialog)
            return node.InstantiatePort(Orientation.Horizontal, portDirection, capacity, typeof(float));
        }

        public override List<Port> GetCompatiblePorts(Port startPort, NodeAdapter nodeAdapter)
        {
            var compatiblePorts = new List<Port>();
            ports.ForEach(port => 
            {
                if (startPort == port || startPort.node == port.node) { }
                else compatiblePorts.Add(port); 
            });
            
            return compatiblePorts;
        }

        public void CreateNode(string nodeName)
        {
            AddElement(CreateDialogNode(nodeName));
        }
        
        public DialogNode CreateDialogNode(string nodeName)
        {
            var node = new DialogNode
            {
                title = nodeName,
                dialogText = nodeName,
                guid = Guid.NewGuid().ToString()
            };
            
            var inputPort = GeneratePort(node, Direction.Input, Port.Capacity.Multi);
            inputPort.portName = "Input";
            node.inputContainer.Add(inputPort);
            
            node.styleSheets.Add(Resources.Load<StyleSheet>("Node"));
            
            var button = new Button(()=> { AddChoicePort(node); });
            button.text = "Add Choice";
            node.titleContainer.Add(button);
            
            var textField = new TextField(string.Empty);
            textField.RegisterValueChangedCallback(evt => 
            {
                node.dialogText = evt.newValue;
                node.title = evt.newValue;
            });
            textField.SetValueWithoutNotify(node.title);
            node.mainContainer.Add(textField);
            
            node.RefreshExpandedState();
            node.RefreshPorts();
            node.SetPosition(new Rect(Vector2.zero, defaultNodeSize));
            
            return node;
        }
        
        public void AddChoicePort(DialogNode node, string overridePortName = "")
        {
            var port = GeneratePort(node, Direction.Output);
            
            var oldLabel = port.contentContainer.Q<Label>("type");
            port.contentContainer.Remove(oldLabel);
            
            var outputPortCount = node.outputContainer.Query("connector").ToList().Count;
            
            var portName = string.IsNullOrEmpty(overridePortName) ?
                $"Choice{outputPortCount}" : 
                overridePortName;
            
            port.portName = portName;
            
            var textField = new TextField
            {
                name = string.Empty,
                value = portName
            };
            textField.RegisterValueChangedCallback(evt => port.portName = evt.newValue);
            port.contentContainer.Add(new Label(" "));
            port.contentContainer.Add(textField);
            
            var deleteButton = new Button(() => RemovePort(node, port))
            {
                text = "X"
            };
            port.contentContainer.Add(deleteButton);
                    
            node.outputContainer.Add(port);
            node.RefreshExpandedState();
            node.RefreshPorts();
        }
        
        void RemovePort(DialogNode node, Port port)
        {
            var targetEdge = edges.ToList().Where(x => 
                x.output.portName == port.portName && x.output.node == port.node);
                
            if (!targetEdge.Any()) return;
            var edge = targetEdge.First();
            edge.input.Disconnect(edge);
            RemoveElement(targetEdge.First());
            
            node.outputContainer.Remove(port);
            node.RefreshPorts();
            node.RefreshExpandedState();
        }
    }
}
