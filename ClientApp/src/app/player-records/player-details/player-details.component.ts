import { Component, OnDestroy, OnInit } from '@angular/core';
import { ActivatedRoute, Params } from '@angular/router';
import { Subscription } from 'rxjs';

import { Player } from 'src/app/shared/models/player.model';
import { PlayersService } from 'src/app/shared/players.service';

@Component({
  selector: 'app-player-details',
  templateUrl: './player-details.component.html',
  styleUrls: ['./player-details.component.scss']
})
export class PlayerDetailsComponent implements OnInit, OnDestroy {
  player: Player | null = null;
  previousName: string;
  name: string;
  categories: string[] = [];
  currentCategory: string;

  subscription: Subscription;

  constructor(
    private route: ActivatedRoute,
    private playersService: PlayersService) { }

  ngOnInit() {
    this.route.params.subscribe(
      (params: Params) => {
        this.name = params['name'];
        this.setPlayer(this.playersService.getPlayer(this.name));
      }
    );

    this.route.queryParams.subscribe(
      (params: Params) => {
        this.currentCategory = params["category"];
      }
    );

    this.subscription = this.playersService.playersChanged.subscribe(
      (players: Player[]) => {
        this.setPlayer(this.playersService.getPlayer(this.name));
      }
    );

    this.setPlayer(this.playersService.getPlayer(this.name));
  }

  setPlayer(player: Player | undefined | null) {
    if (!player) {
      this.player = null;
      return;
    }

    this.player = player;

    this.categories = [];
    player.advancements.forEach(advancement => {
      if (!this.categories.find((element) => element === advancement.category))
        this.categories.push(advancement.category);
    });

    if (this.previousName != this.name) {
      this.previousName = this.name;
      
      

      this.playersService.loadPlayer(player.name);
    }
  }

  filterAdvancements() {
    if (!this.currentCategory)
      return this.player?.advancements;
    return this.player?.advancements.filter((element) => element.category === this.currentCategory);
  }

  ngOnDestroy() {
    this.subscription.unsubscribe();
  }
}
