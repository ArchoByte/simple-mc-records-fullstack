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
import { PlayerSkinComponent } from './shared/components/player-skin/player-skin.component';
import { PlayerHeadComponent } from './shared/components/player-skin/player-head/player-head.component';
import { PlayerHandComponent } from './shared/components/player-skin/player-hand/player-hand.component';
import { PlayerBodyComponent } from './shared/components/player-skin/player-body/player-body.component';
import { PlayerLegComponent } from './shared/components/player-skin/player-leg/player-leg.component';
import { LoadingSpinnerComponent } from './shared/components/loading-spinner/loading-spinner.component';
import { ScoreItemComponent } from './player-records/player-details/score-item/score-item.component';
import { AdvancementItemComponent } from './player-records/player-details/advancement-item/advancement-item.component';
import { UnderscoreToSpacePipe } from './shared/pipes/underscore-to-space.pipe';
import { AdvancementIconComponent } from './shared/components/advancement-icon/advancement-icon.component';
import { CategoryItemComponent } from './player-records/player-details/category-item/category-item.component';
import { CapitalizeFirstPipe } from './shared/pipes/capitalize-first.pipe';

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
    PlayerLegComponent,
    LoadingSpinnerComponent,
    ScoreItemComponent,
    AdvancementItemComponent,
    UnderscoreToSpacePipe,
    AdvancementIconComponent,
    CategoryItemComponent,
    CapitalizeFirstPipe
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
