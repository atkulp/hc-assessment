import { TestBed, async } from '@angular/core/testing';
import { RouterTestingModule } from '@angular/router/testing';
import { RouterModule } from '@angular/router'
import { FormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { AssessmentAppComponent } from './app.component';
import {
    PeopleFinderService, NavMenuComponent, HomeComponent, SearchBoxComponent,
    SearchResultComponent, AboutComponent, SearchStatus } from '.';

describe('AssessmentAppComponent', () => {
  beforeEach(async(() => {
    TestBed.configureTestingModule({
      imports: [
        RouterTestingModule,
        HttpClientModule,
        FormsModule,
        RouterModule.forRoot([
          { path: '', component: HomeComponent, pathMatch: 'full' },
          { path: 'About', component: AboutComponent, pathMatch: 'full' }
        ])    
      ],
      declarations: [
        AssessmentAppComponent,
        NavMenuComponent,
        HomeComponent,
        AboutComponent,
        SearchBoxComponent,
        SearchResultComponent
      ],
      providers: [
          PeopleFinderService,
        { provide: 'BASE_URL', useFactory: () => "/", deps: [] }
      ]
    }).compileComponents();
  }));

  it('should create the app', () => {
    const fixture = TestBed.createComponent(AssessmentAppComponent);
    const app = fixture.debugElement.componentInstance;
    expect(app).toBeTruthy();
  });

  it(`should have as title 'app'`, () => {
    const fixture = TestBed.createComponent(AssessmentAppComponent);
    const app = fixture.debugElement.componentInstance;
    expect(app.title).toEqual('app');
  });
});

describe('PeopleFinderService', () => {
  beforeEach(async(() => {
    TestBed.configureTestingModule({
      imports: [
        HttpClientModule,
      ],
      declarations: [
      ],
      providers: [
          PeopleFinderService,
        { provide: 'BASE_URL', useFactory: () => "/", deps: [] }
      ]
    }).compileComponents();
  }));

  it('should create the PeopleFinder', (  ) => {
      const pfc = TestBed.get(PeopleFinderService) as PeopleFinderService;
    expect(pfc).toBeTruthy();
  });

  it(`should start with null results`, () => {
      const pfc = TestBed.get(PeopleFinderService) as PeopleFinderService;
    expect(pfc.people).toBeNull();
  });

  it('should start in Idle state', () => {
      const pfc = TestBed.get(PeopleFinderService) as PeopleFinderService;
    expect(pfc.state).toEqual(SearchStatus.Idle);
  });

  it('should not search on blank search term', () => {
      const pfc = TestBed.get(PeopleFinderService) as PeopleFinderService;
    pfc.search(null);
    expect(pfc.state).toEqual(SearchStatus.Idle);
  });
});
