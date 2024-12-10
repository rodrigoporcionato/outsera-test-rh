import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';


@Injectable({
  providedIn: 'root'
})
export class MovieService {
  private apiUrl = 'https://challenge.outsera.tech/api/movies';

  constructor(private http: HttpClient) {}

  getMovies(page: number = 1, size: number = 10, winner: boolean = false, year?: number): Observable<any> {

    let url = `${this.apiUrl}?page=${page}&size=${size}&winner=${winner}`;
    if (year) {//api retorna de forma dirente quando usado filtro por ano. por isso foi removido o page e size pq nao está funcionando.
      url = `${this.apiUrl}?winner=${winner}`
      url += `&year=${year}`;
    }
    return this.http.get<any>(url);
  }

  getYearsWithMultipleWinners(): Observable<any> {
    const url = `${this.apiUrl}?projection=years-with-multiple-winners`;
    return this.http.get<any>(url);
  }

  getStudiosWithWinCount(): Observable<any> {
    const url = `${this.apiUrl}?projection=studios-with-win-count`;
    return this.http.get<any>(url);
  }

  getProducersWithWinInterval(): Observable<any> {
    const url = `${this.apiUrl}?projection=max-min-win-interval-for-producers`;
    return this.http.get<any>(url);
  }

  getMoviesByYear(year: number): Observable<any> {
    const url = `${this.apiUrl}?winner=true&year=${year}`;
    return this.http.get<any>(url);
  }
}

