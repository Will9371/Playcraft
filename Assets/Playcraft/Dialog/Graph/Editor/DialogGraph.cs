// Credit: Mert Kirimgeri
// Source: https://www.youtube.com/watch?v=7KHGH0fPL84

using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace Playcraft.Experimental.DialogGraph
{
    public class DialogGraph : EditorWindow
    {
        DialogGraphView graphView;
        string fileName = "New Narrative";

        [MenuItem("Graph/Dialog Graph")]
        public static void OpenDialogGraphWindow()
        {
            var window = GetWindow<DialogGraph>();
            window.titleContent = new GUIContent("Dialog Graph");
        }
        
        void OnEnable()
        {
            ConstructGraphView();
            GenerateToolbar();
            GenerateMiniMap();
        }
        
        void ConstructGraphView()
        {
            graphView = new DialogGraphView
            {
                name = "Dialog Graph"
            };
            
            graphView.StretchToParentSize();
            rootVisualElement.Add(graphView);        
        }
        
        void GenerateToolbar()
        {
            var toolbar = new Toolbar();
            
            var fileNameTextField = new TextField("File Name:");
            fileNameTextField.SetValueWithoutNotify(fileName);
            fileNameTextField.MarkDirtyRepaint();
            fileNameTextField.RegisterValueChangedCallback(evt => fileName = evt.newValue);
            toolbar.Add(fileNameTextField);
            
            toolbar.Add(new Button(() => RequestDataOperation(true)) { text = "Save Data" });
            toolbar.Add(new Button(() => RequestDataOperation(false)) { text = "Load Data" });
            
            var createNodeButton = new Button(() => { graphView.CreateNode("Dialog Node"); });
            createNodeButton.text = "Create Node";
            toolbar.Add(createNodeButton);
            
            rootVisualElement.Add(toolbar);
        }
        
        void RequestDataOperation(bool save)
        {
            if (string.IsNullOrEmpty(fileName))
            {
                EditorUtility.DisplayDialog("Invalid file name", "Please enter a valid file name", "OK");
                return;
            }
            
            var saveUtility = GraphSaveUtility.GetInstance(graphView);
            
            if (save) saveUtility.SaveGraph(fileName);
            else saveUtility.LoadGraph(fileName);
        }
        
        void GenerateMiniMap()
        {
            var miniMap = new MiniMap{ anchored = true };
            miniMap.SetPosition(new Rect(10, 30, 200, 140));
            graphView.Add(miniMap);
        }
        
        void OnDisable()
        {
            rootVisualElement.Remove(graphView);
        }
    }
}
