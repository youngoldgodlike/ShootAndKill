﻿using UnityEngine;

public class AbilitySystem : MonoBehaviour {
    [SerializeField] AbilityView view;
    [SerializeField] AbilityData[] startingAbilities;
    AbilityController controller;

    void Awake() {
        controller = new AbilityController.Builder()
            .WithAbilities(startingAbilities)
            .Build(view);
    }

    void Update() => controller.Update(Time.deltaTime);
}