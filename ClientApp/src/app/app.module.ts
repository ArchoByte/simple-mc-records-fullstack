import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule } from '@angular/common/http';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { PlayerRecordsComponent } from './player-records/player-records.component';
import { PlayerListComponent } from './player-records/player-list/player-list.component';
import { PlayerItemComponent } from './player-records/player-list/player-item/player-item.component';
import { PlayerDetailsComponent } from './player-records/player-details/player-details.component';
import { PlayerStartComponent } from './player-records/player-start/player-start.component';
import { HeaderComponent } from './header/header.component';

@NgModule({
  declarations: [
    AppComponent,
    PlayerRecordsComponent,
    PlayerListComponent,
    PlayerItemComponent,
    PlayerDetailsComponent,
    PlayerStartComponent,
    HeaderComponent
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    AppRoutingModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
