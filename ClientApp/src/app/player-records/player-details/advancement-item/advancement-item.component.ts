import { Component, Input } from '@angular/core';
import { Advancement } from 'src/app/shared/models/advancement.model';

@Component({
  selector: 'app-advancement-item',
  templateUrl: './advancement-item.component.html'
})
export class AdvancementItemComponent {
  @Input() advancement: Advancement;
}
