import { Score } from './score.model';
import { Advancement } from './advancement.model';

export class Player {
    constructor(
        public name: string,
        public texturePath: string,
        public scores: Score[],
        public advancements: Advancement[]
    ) { }
}