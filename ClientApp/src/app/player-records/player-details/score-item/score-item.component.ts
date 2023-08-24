import { Component, Input } from '@angular/core';
import { Score } from 'src/app/shared/models/score.model';

@Component({
  selector: 'app-score-item',
  templateUrl: './score-item.component.html'
})
export class ScoreItemComponent {
  @Input() score: Score;
}
