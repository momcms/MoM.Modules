﻿import { Component, Host } from "@angular/core";
import { NgFormModel } from "@angular/common";
import { ValidationService } from "./validation.service";

@Component({
    selector: "control-messages",
    inputs: ["controlName: control"],
    template: `<div *ngIf="errorMessage !== null" class="form-control alert-danger">{{errorMessage}}</div>`
})
export class ControlMessages {
    controlName: string;
    constructor( @Host() private _formDir: NgFormModel) { }

    get errorMessage() {
        let c = this._formDir.form.find(this.controlName);

        for (let propertyName in c.errors) {
            if (c.errors.hasOwnProperty(propertyName) && c.touched && !c.pristine) {
                return ValidationService.getValidatorErrorMessage(propertyName);
            }
        }

        return null;
    }
}