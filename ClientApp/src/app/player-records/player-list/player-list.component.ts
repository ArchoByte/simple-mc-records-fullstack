import { Component, OnDestroy, OnInit } from '@angular/core';
import { Subscription } from 'rxjs';

import { Player } from 'src/app/shared/models/player.model';
import { PlayersService } from 'src/app/shared/players.service';

@Component({
  selector: 'app-player-list',
  templateUrl: './player-list.component.html',
  styleUrls: ['./player-list.component.scss']
})
export class PlayerListComponent implements OnInit, OnDestroy {
  players: Player[];
  subscription: Subscription;

  constructor(private playersService: PlayersService) { }

  ngOnInit() {
    this.subscription = this.playersService.playersChanged.subscribe(
      (players: Player[]) => {
        this.players = players;
      }
    );
    this.players = this.playersService.getPlayers();
  }

  ngOnDestroy() {
    this.subscription.unsubscribe();
  }
}
