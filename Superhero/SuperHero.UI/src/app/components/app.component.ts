import { SuperHeroService } from './../services/super-hero.service';
import { Component } from '@angular/core';
import { SuperHero } from '../models/super-hero';

@Component({
  selector: 'app-root',
  templateUrl: './../components/app.component.html',
  styleUrls: ['./../components/app.component.css']
})
export class AppComponent {
  title = 'SuperHero.UI';
  heroes: SuperHero[] = [];
  heroToEdit?: SuperHero;
  
  constructor(private superHeroService : SuperHeroService) { }
  
    ngOnInit() : void {
      this.superHeroService
      .getSuperHeroes()
      .subscribe((result : SuperHero[]) => (this.heroes = result));
    }
  
    updateHeroList(heroes: SuperHero[]) {
      this.heroes = heroes;
    }
  
    initNewHero() {
      this.heroToEdit = new SuperHero();
    }
  
    editHero(hero: SuperHero) {
      this.heroToEdit = hero;
    }
  }