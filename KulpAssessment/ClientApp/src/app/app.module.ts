import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router'
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';

import { AssessmentAppComponent } from './app.component';
import { HomeComponent, AboutComponent, PeopleFinderService, NavMenuComponent, SearchBoxComponent, SearchResultComponent} from '.'

@NgModule({
  imports: [
    BrowserModule,
    FormsModule,
    HttpClientModule,
    RouterModule.forRoot([
      { path: '', component: HomeComponent, pathMatch: 'full' },
      { path: 'About', component: AboutComponent, pathMatch: 'full' }
    ])
  ],
  declarations: [
    HomeComponent,
    AboutComponent,
    NavMenuComponent,
    AssessmentAppComponent,
    SearchBoxComponent,
    SearchResultComponent
  ],
  providers: [
      PeopleFinderService
  ],
  bootstrap: [AssessmentAppComponent]
})
export class AppModule { }
