﻿using System.Collections.Generic;
using UnityEngine.EventSystems;

public class AbilityController {
    readonly AbilityModel model; // главный за данные
    readonly AbilityView view; // главный за интерфейс
    readonly Queue<AbilityCommand> abilityQueue = new();
    // readonly CountdownTimer timer = new CountdownTimer(0); 

    AbilityController(AbilityView view, AbilityModel model) {
        this.view = view;
        this.model = model;

        ConnectModel();
        ConnectView();
    }
    
    void ConnectModel() {
        model.abilities.AnyValueChanged += UpdateButtons;
    }
    
    void ConnectView() {
        for (int i = 0; i < view.buttons.Length; i++) {
            view.buttons[i].RegisterListener(OnAbilityButtonPressed);
        }
        view.UpdateButtonSprites(model.abilities);
    }
    
    public void Update(float deltaTime) {
        //timer.Tick(deltaTime); таймер тик так
        var timerProgress = 0.02f;
        view.UpdateRadial(timerProgress);

        bool timerRunning = false;
        
        if (!timerRunning && abilityQueue.TryDequeue(out AbilityCommand cmd)) {
            cmd.Execute();
            //timer.Reset(cmd.duration);
            //timer.Start();
        }
    }
    
    void UpdateButtons(IList<Ability> updatedAbilities) => view.UpdateButtonSprites(updatedAbilities);

    void OnAbilityButtonPressed(int index) {
        //if (timer.Progress < 0.25f || !timer.IsRunning) {
            if (model.abilities[index] != null) {
                abilityQueue.Enqueue(model.abilities[index].CreateCommand());
            }
        //}
        EventSystem.current.SetSelectedGameObject(null);
    }

    public class Builder {
        readonly AbilityModel model = new AbilityModel();
        
        public Builder WithAbilities(AbilityData[] datas) {
            foreach (var data in datas) {
                model.Add(new Ability(data));
            }
            return this;
        }

        public AbilityController Build(AbilityView view) {
            //Preconditions.CheckNotNull(view); Check for null
            return new AbilityController(view, model);
        }
    }
}