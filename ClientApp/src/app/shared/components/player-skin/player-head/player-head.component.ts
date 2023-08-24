import { Component, Input } from '@angular/core';

@Component({
  selector: 'app-player-head',
  templateUrl: './player-head.component.html'
})
export class PlayerHeadComponent {
  @Input() texturePath : string;
  @Input() size : number;
}
