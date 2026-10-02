using UnityEngine;

public class GodMenu_Presenter
{
    private readonly GodMenu_Model _model;
    private readonly GodMenu_View _view;
    private Dialogue_Controller Dialogue_Controller;
    private Character_Database _database;


    public GodMenu_Presenter(GodMenu_Model model, GodMenu_View view, Dialogue_Controller controller)
    {
        _model = model; 
        _view = view;

        // Subscribe to view events 
        _view.OnCharacterChanged += HandleCharacterChanged;
        _view.InitializeCharacterDropDown(_model.CharacterOptions);
        _view.UpdateGraphicDropdownSelection(_model.CharacterIndex);
    }



    private void HandleCharacterChanged(int index)
    {
        // Save to model 
        _model.SetActiveCharacter(index);
        Characater_Data data = _database.GetCharacter(index.ToString());
        Dialogue_Controller.inkFile = data.ScriptJson;
    }

    public void Dispose()
    {
        _view.OnCharacterChanged -= HandleCharacterChanged;
    }
}
