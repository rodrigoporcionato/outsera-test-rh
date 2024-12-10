
import { Component } from '@angular/core';
import { MovieService } from '../services/movie.service'; // Serviço para consumir a API
import { FormsModule } from '@angular/forms'; // Adicione FormsModule aqui
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.scss'],
  imports: [FormsModule, CommonModule]
})
export class DashboardComponent {
  yearsWithMultipleWinners: any[] = [];
  studiosWithWins: any[] = [];
  producersWithIntervals: any = {};
  selectedYear: number | null = null;
  filteredMovies: any[] = [];

  constructor(private movieService: MovieService) {}

  ngOnInit(): void {
    this.loadYearsWithMultipleWinners();
    this.loadTopStudiosWithWins();
    this.loadProducersWithIntervals();
  }

  loadYearsWithMultipleWinners(): void {
    this.movieService.getYearsWithMultipleWinners().subscribe(data => {
      this.yearsWithMultipleWinners = data.years;
    });
  }

  loadTopStudiosWithWins(): void {
    this.movieService.getStudiosWithWinCount().subscribe(data => {
      this.studiosWithWins = data.studios.slice(0, 3); // Pega apenas os 3 primeiros estúdios
    });
  }

  loadProducersWithIntervals(): void {
    this.movieService.getProducersWithWinInterval().subscribe(data => {
      this.producersWithIntervals = data;
    });
  }

  searchByYear(): void {
    if (this.selectedYear) {
      this.movieService.getMoviesByYear(this.selectedYear).subscribe(data => {

        this.filteredMovies = data;
        console.log(this.filteredMovies);
      });
    }
  }
}
