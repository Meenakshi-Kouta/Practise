import { environment } from 'src/environments/environment';
import { Injectable } from '@angular/core';
import { SuperHero } from '../models/super-hero';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs/internal/Observable';

@Injectable({
  providedIn: 'root'
})
export class SuperHeroService {
 url = "SuperHero";
  constructor(private http: HttpClient) { }

public getSuperHeroes() : Observable<SuperHero[]> {
  return this.http.get<SuperHero[]>(`${environment.apiUrl}/${this.url}`);
}
public createSuperHeroes(hero: SuperHero) : Observable<SuperHero[]> {
  return this.http.post<SuperHero[]>(
    `${environment.apiUrl}/${this.url}`,
    hero
  );
}
public updateSuperHeroes(hero: SuperHero) : Observable<SuperHero[]> {
  return this.http.put<SuperHero[]>(
    `${environment.apiUrl}/${this.url}`,
    hero
    );
}
public deleteSuperHeroes(hero: SuperHero) : Observable<SuperHero[]> {
  return this.http.delete<SuperHero[]>(
    `${environment.apiUrl}/${this.url}/${hero.id}`
    );
}
}
