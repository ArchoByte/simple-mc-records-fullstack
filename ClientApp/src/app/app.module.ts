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
import { PlayerSkinComponent } from './shared/player-skin/player-skin.component';
import { PlayerHeadComponent } from './shared/player-skin/player-head/player-head.component';
import { PlayerHandComponent } from './shared/player-skin/player-hand/player-hand.component';
import { PlayerBodyComponent } from './shared/player-skin/player-body/player-body.component';
import { PlayerLegComponent } from './shared/player-skin/player-leg/player-leg.component';

@NgModule({
  declarations: [
    AppComponent,
    PlayerRecordsComponent,
    PlayerListComponent,
    PlayerItemComponent,
    PlayerDetailsComponent,
    PlayerStartComponent,
    HeaderComponent,
    PlayerSkinComponent,
    PlayerHeadComponent,
    PlayerHandComponent,
    PlayerBodyComponent,
    PlayerLegComponent
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
