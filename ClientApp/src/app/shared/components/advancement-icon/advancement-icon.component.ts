import { Component, Input } from '@angular/core';

@Component({
  selector: 'app-advancement-icon',
  templateUrl: './advancement-icon.component.html'
})
export class AdvancementIconComponent {
  @Input() category: string;
}
