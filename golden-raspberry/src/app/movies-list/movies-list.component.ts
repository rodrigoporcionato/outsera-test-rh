import { Component, OnInit } from '@angular/core';
import { MovieService } from '../services/movie.service';
import { FormsModule } from '@angular/forms'; // Adicione FormsModule aqui
import { HttpClient } from '@angular/common/http'; // Importando HttpClientModule
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-movies-list',
  templateUrl: './movies-list.component.html',
  styleUrls: ['./movies-list.component.scss'],
  imports:[FormsModule, CommonModule]
})
export class MoviesListComponent implements OnInit {
  movies: any[] = [];
  filterYear: number | undefined = undefined;
  filterWinner: boolean | null = null;
  currentPage: number = 1;
  totalPages: number = 1;
  sortDirectionYear: boolean = true;
  sortDirectionWinner: boolean = true;

  constructor(private movieService: MovieService) {}

  ngOnInit(): void {
    this.loadMovies();
  }

  loadMovies(): void {
    const winnerFilter = this.filterWinner === null ? false : this.filterWinner;
    const yearFilter = this.filterYear === undefined ? undefined : this.filterYear;

    this.movieService.getMovies(this.currentPage, 10, winnerFilter, yearFilter).subscribe({
      next: (data) => {

        console.log('Data received:', data);
        //a api tem um erro. quando se filtra por year, ele nao retorna content!
        //workarround
        this.movies = (yearFilter != undefined && yearFilter > 0 ? data: data.content);
        this.totalPages = data.totalPages;
      },
      error: (error) => {
        console.error('Error fetching movies:', error);
      }
    });


  }

  filterMovies(): void {
    this.loadMovies();
  }

  // Paginação
  previousPage(): void {
    if (this.currentPage > 1) {
      this.currentPage--;
      this.loadMovies();
    }
  }

  nextPage(): void {
    if (this.currentPage < this.totalPages) {
      this.currentPage++;
      this.loadMovies();
    }
  }
}
