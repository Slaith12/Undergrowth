using Ink.Parsed;
using UnityEngine;
using System.Collections.Generic;

public class GodMenu_Bootstrap : MonoBehaviour
{
    [SerializeField]
    private GodMenu_View _view;
    private GodMenu_Presenter GodMenu_Presenter;
    [SerializeField]
    private Character_Database character_database;
    [SerializeField]
    private Dialogue_Controller dialogue_controller;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GodMenu_Model _model = new GodMenu_Model(0, "Default",character_database);
        GodMenu_Presenter = new GodMenu_Presenter(_model, _view,dialogue_controller);
    }

    // Update is called once per frame
    void Update()
    {
        GodMenu_Presenter?.Dispose();
    }
}
