import { Component, Input } from '@angular/core';

@Component({
  selector: 'app-player-hand',
  templateUrl: './player-hand.component.html'
})
export class PlayerHandComponent {
  @Input() texturePath : string;
  @Input() size : number;
}
