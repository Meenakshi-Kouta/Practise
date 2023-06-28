import { SuperHero } from './../../models/super-hero';
import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { SuperHeroService } from 'src/app/services/super-hero.service';

@Component({
  selector: 'app-edit-hero',
  templateUrl: './edit-hero.component.html',
  styleUrls: ['./edit-hero.component.css']
})
export class EditHeroComponent implements OnInit{
@Input() hero?: SuperHero;
@Output() heroesUpdated = new EventEmitter<SuperHero[]>();

constructor(private superHeroService: SuperHeroService) {}
ngOnInit(): void {}
createHero(hero: SuperHero) {
  this.superHeroService
  .createSuperHeroes(hero)
  .subscribe((heroes: SuperHero[]) => this.heroesUpdated.emit(heroes));
  }
updateHero(hero: SuperHero) {
this.superHeroService
.updateSuperHeroes(hero)
.subscribe((heroes: SuperHero[]) => this.heroesUpdated.emit(heroes));
}
deleteHero(hero: SuperHero) {
  this.superHeroService
  .deleteSuperHeroes(hero)
  .subscribe((heroes: SuperHero[]) => this.heroesUpdated.emit(heroes));
  }
}