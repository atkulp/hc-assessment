import { Component, Input } from '@angular/core';
import { IPerson } from '..';

@Component({
  selector: 'search-result',
  templateUrl: 'search-result.component.html',
  styleUrls: ['search-result.component.css']
})
export class SearchResultComponent {
    /**
     * Person to display for this result
     */
    @Input() person: IPerson;

    /**
     * Compute age of person by finding different of DOB to today then going from ms to years
     */
    getAgeDisplay(): string {
        // Switch age calculation if deceased
        if (this.person.dod) {
            let ageAtDeath = Math.floor((this.person.dod.getTime() - this.person.dob.getTime()) / 1000 / 60 / 60 / 24 / 365);
            return `${ageAtDeath} yr${ageAtDeath !== 1 ? "s" : ""} (at death)`;
        }
        else {
            let ageNow = Math.floor(((new Date()).getTime() - this.person.dob.getTime()) / 1000 / 60 / 60 / 24 / 365);
            return `${ageNow} yr${ageNow !== 1 ? "s" : ""}`;
        }
    }

    /**
     * Get actual avatar or use placeholder
     */
    getAvatar() : string {
        return this.person.avatarUrl || `https://ui-avatars.com/api/?name=${this.person.firstName}+${this.person.lastName}&size=200`
    }
}
