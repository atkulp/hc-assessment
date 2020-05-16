import { Component, Input } from '@angular/core';
import { PeopleFinderService } from '../people-finder/finder.service' 
import { SearchStatus } from "../people-finder/SearchStatus";

@Component({
    selector: 'search-box',
    templateUrl: './search-box.component.html'
})
export class SearchBoxComponent {
    term: string;

    // Make enum accessible.  Could also use custom decorator to insert it 
    SearchStatus = SearchStatus;
    finder: PeopleFinderService;

    constructor(finderComponent: PeopleFinderService) {
        this.finder = finderComponent;
    }

    search() {
        this.finder.search(this.term);
    }
}
