﻿import {Component, OnInit} from "@angular/core";
import {TAB_DIRECTIVES} from "ng2-bootstrap/ng2-bootstrap";

@Component({
    selector: "mom-admin-reports",
    directives: [TAB_DIRECTIVES],
    templateUrl: "/cms/pages/adminreports"
})
export class AdminReportsComponent implements OnInit {
    //message: string;

    //constructor() { }

    ngOnInit() {
        //this.message = "Welcome to EasyModules.NET"
    }
}