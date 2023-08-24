import { Injectable } from '@angular/core';
import { Subject } from 'rxjs';

import { Player } from './models/player.model';
import { DataStorageService } from './data-storage.service';

@Injectable({providedIn: 'root'})
export class PlayersService {
  playersChanged = new Subject<Player[]>();

  private players: Player[] = [];

  // TODO: implement DataStorageService
  constructor(private dataStorageService: DataStorageService) {
    this.dataStorageService.fetchPlayers().subscribe(players => {
      this.setPlayers(players);
      players.forEach(player => {
        this.dataStorageService.loadTexture(player.name).subscribe(texture => {
          player.texturePath = texture;
        });
      });
    });
  }

  setPlayers(players: Player[]) {
    this.players = players;
    this.playersChanged.next(this.players.slice());
  }

  getPlayers() {
    return this.players.slice();
  }

  getPlayer(name: string) {
    return this.players.find((element) => element.name === name);
  }

  loadPlayer(name: string) {
    let player = this.getPlayer(name);
    if (!player)
      return;

    if (!player.scores || player.scores.length <= 0) {
      this.dataStorageService.loadScores(player.name).subscribe(scores => {
        if (!player)
          return;
        player.scores = scores;
        this.playersChanged.next(this.players.slice());
      });
    }

    if (!player.advancements || player.advancements.length <= 0) {
      this.dataStorageService.loadAdvancements(player.name).subscribe(advancements => {
        if (!player)
          return;
        player.advancements = advancements;
        this.playersChanged.next(this.players.slice());
      });
    }
  }
}
