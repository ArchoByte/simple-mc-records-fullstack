import { Component, Input } from '@angular/core';

@Component({
  selector: 'app-player-skin',
  templateUrl: './player-skin.component.html'
})
export class PlayerSkinComponent {
  @Input() texturePath : string;
  @Input() size : number;
}
