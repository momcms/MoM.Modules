﻿import { NgModule }       from "@angular/core";
import { BrowserModule }  from "@angular/platform-browser";

/* App Root */
import { AppComponent }   from "./app.component";

/* Feature Modules */
import { HomeModule }    from "./pages/home/home.module";
//import { CoreModule }       from "./core/core.module";

/* Routing Module */
import { AppRoutingModule } from "./app-routing.module";
/* Bootstrap for Angular 2 */
import { Ng2BootstrapModule } from "ng2-bootstrap/ng2-bootstrap";

@NgModule({
    imports: [
        BrowserModule,
        HomeModule,
        /*
            CoreModule,
        */
        //CoreModule.forRoot({userName: "Miss Marple"}),
        AppRoutingModule,
        Ng2BootstrapModule
    ],
    declarations: [AppComponent],
    bootstrap: [AppComponent]
})
export class AppModule { }


/*
Copyright 2016 Google Inc. All Rights Reserved.
Use of this source code is governed by an MIT-style license that
can be found in the LICENSE file at http://angular.io/license
*/