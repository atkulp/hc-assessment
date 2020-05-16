import { Component, Input } from '@angular/core';
import { PeopleFinderService } from '../people-finder/finder.service' 
import { SearchStatus } from "../people-finder/SearchStatus";

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html'
})
export class HomeComponent {
    // Make enum accessible.  Could also use custom decorator to insert it 
    SearchStatus = SearchStatus;
    finder: PeopleFinderService;

    constructor(finderComponent: PeopleFinderService){
        this.finder = finderComponent;
    }
}