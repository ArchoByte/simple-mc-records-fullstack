import { Component, Input } from '@angular/core';

@Component({
  selector: 'app-category-item',
  templateUrl: './category-item.component.html'
})
export class CategoryItemComponent {
  @Input() category: string;
}
