import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { map, switchMap } from 'rxjs/operators';

import { Player } from './models/player.model';
import { Score } from './models/score.model';
import { Advancement } from './models/advancement.model';

@Injectable({ providedIn: 'root' })
export class DataStorageService {
  constructor(private http: HttpClient) { }
  private playerPath = 'api/Players';
  private scorePath = 'api/Scores';

  private profilesPath = '/users/profiles/minecraft';
  private texturesPath = '/session/minecraft/profile';

  fetchPlayers() {
    return this.http.get<Player[]>(
      this.playerPath
    ).pipe(
      map(players => {
        return players.map(player => {
          return {
            ...player,
            scores: player.scores ? player.scores : [],
            advancements: player.advancements ? player.advancements : []
          };
        });
      })
    );
  }

  loadTexture(playerName: string) {
    return this.http.get<{ name: string, id: string }>(this.profilesPath + '/' + playerName).pipe(
      map(profile => profile.id),
      switchMap(uuid => {
        return this.http.get<{ name: string, id: string, properties: [{ name: string, value: string }] }>(this.texturesPath + '/' + uuid).pipe(
          map(response => {
            let jsonStr = atob(response.properties[0].value);
            let json = JSON.parse(jsonStr);
            return String(json.textures.SKIN.url);
          })
        );
      })
    );
  }

  loadScores(playerName: string) {
    return this.http.get<Score[]>(
      this.playerPath + '/' + playerName + '/Scores'
    );
  }

  loadAdvancements(playerName: string) {
    return this.http.get<{ name: string, categoryName: string, time: Date}[]>(
      this.playerPath + '/' + playerName + '/Advancements'
    ).pipe(
      map(response => {
        return response.map<Advancement>(advancementRaw => {
          return new Advancement(
            advancementRaw.name,
            advancementRaw.categoryName,
            advancementRaw.time
          );
        });
      })
    );
  }
}
