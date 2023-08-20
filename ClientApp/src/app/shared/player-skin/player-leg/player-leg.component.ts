import { Component, Input } from '@angular/core';

@Component({
  selector: 'app-player-leg',
  templateUrl: './player-leg.component.html'
})
export class PlayerLegComponent {
  @Input() texturePath : string;
  @Input() size : number;
}
