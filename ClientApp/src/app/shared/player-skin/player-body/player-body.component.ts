import { Component, Input } from '@angular/core';

@Component({
  selector: 'app-player-body',
  templateUrl: './player-body.component.html'
})
export class PlayerBodyComponent {
  @Input() texturePath : string;
  @Input() size : number;
}
