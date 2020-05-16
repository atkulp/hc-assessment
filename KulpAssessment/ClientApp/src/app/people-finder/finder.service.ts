import { Inject, Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { SearchStatus } from './SearchStatus';
import { IPerson } from './IPerson';

@Injectable()
export class PeopleFinderService {
    people: IPerson[] = null;
    state: SearchStatus = SearchStatus.Idle;

    private errorTimeout: number;

    constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string) {
    }

    search(querystring: string) {

        // Trim local copy (server will too, but just to be safe)
        querystring = querystring ? querystring.trim() : null;

        // Don't double-search or search with nothing
        if( this.state != SearchStatus.Idle || !querystring ) return;

        // Clear results while searching since list is no longer valid
        this.people = null;

        // Clear error state if necessary.  Don't want that timer going off  
        if( this.errorTimeout) {
            window.clearTimeout(this.errorTimeout);
            this.errorTimeout = undefined;
        }

        // Indicate entering search state
        this.state = SearchStatus.Searching;

        // Give the search one second before calling it slow.  Better user experience
        let t = window.setTimeout(() => {
            // If we fire, the search is taking too long...
            this.state = SearchStatus.SearchingSlow; 
        }, 1000);

        this.http.get<IPerson[]>(`${this.baseUrl}person?q=${querystring}`).subscribe(result => {
            // Results are back.  Cancel slow check
            window.clearTimeout(t);

            // Bit of a hack... Could change server date format, but this seems more universal
            result.forEach(p => {
                p.dob = new Date(p.dob);
                p.dod = p.dod ? new Date(p.dod) : null;
            });

            // Hand off the results
            this.people = result;

            // Nothing happening anymore
            this.state = SearchStatus.Idle;
        }, error => {
            // Results are back.  Cancel slow check
            window.clearTimeout(t);

            // Could go with null since empty could imply good search with
            // no results, it's a bit arbitrary though so let's call it empty.
            this.people = [];

            // Indicate our error. 
            this.state = SearchStatus.Error;

            // Clear the error message after two seconds, but make sure
            // it hasn't changed to something else.
            this.errorTimeout = window.setTimeout(() => this.state = SearchStatus.Error ? SearchStatus.Idle : this.state, 2000 )
        });
    }
}
