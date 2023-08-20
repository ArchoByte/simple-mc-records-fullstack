import { Injectable } from '@angular/core';
import { Subject } from 'rxjs';

import { Player } from './player.model';

@Injectable({providedIn: 'root'})
export class PlayersService {
  playersChanged = new Subject<Player[]>();

  private players: Player[] = [
    new Player(
      'Dream',
      'http://textures.minecraft.net/texture/e33f240cbeeb6d165d3a1182848a3d07859fe650734e7a568ef09a36fffe0efc',
      [],
      []
    ),
    new Player(
      'GeorgeNotFound',
      'http://textures.minecraft.net/texture/2d7552678058720f8920bcee682ac4e7475e41e2155ae6700b2a58389f5b64f6',
      [],
      []
    ),
    new Player(
      'Sapnap',
      'http://textures.minecraft.net/texture/bf6a0be52a25884fad8c2a28e9e223d98544eacf91a55b52fc4f22a11d657db1',
      [],
      []
    )
  ];

  // TODO: implement DataStorageService
  // constructor(private dataStorageService: DataStorageService) {
  //   this.dataStorageService.fetchPlayers().subscribe(players => {
  //     this.setPlayers(players);
  //   });
  // }

  constructor() {
    this.playersChanged.next(this.players.slice());
  }

  setPlayers(players: Player[]) {
    this.players = players;
    this.playersChanged.next(this.players.slice());
  }

  getPlayers() {
    return this.players.slice();
  }

  getPlayer(name: string) {
    return this.players.find((element) => element.name == name);
  }
}
