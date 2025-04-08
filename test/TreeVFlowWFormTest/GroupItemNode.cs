using TreeVFlowControl.Imp;

namespace TreeVFlowWFormTest;

public class GroupItemNode:TreeVFlowNode
{
    private bool _isNodeEnabled;

    public GroupItemNode()
    {
        Init();
    }

    private void Init()
    {
        
    }
    public void DisableNode()
    {
        if (!_isNodeEnabled) return;
        
    }
    
    public void EnableNode()
    {
        if (_isNodeEnabled) return;
    }
}