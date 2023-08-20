import { Component, Input } from '@angular/core';
import { Player } from 'src/app/shared/player.model';

@Component({
  selector: 'app-player-item',
  templateUrl: './player-item.component.html'
})
export class PlayerItemComponent {
  @Input() player: Player;
}
