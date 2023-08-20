import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { PlayerRecordsComponent } from './player-records/player-records.component';
import { PlayerStartComponent } from './player-records/player-start/player-start.component';
import { PlayerDetailsComponent } from './player-records/player-details/player-details.component';

const routes: Routes = [
  { path: '', redirectTo: "/players", pathMatch: 'full' },
  {
    path: 'players', component: PlayerRecordsComponent, children: [
      { path: '', component: PlayerStartComponent },
      { path: ':name', component: PlayerDetailsComponent}
    ]
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
