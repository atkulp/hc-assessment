import { HomePage } from './search.po';

describe('Assessment Search', () => {
  let page: HomePage;

  beforeEach(() => {
    page = new HomePage();
    page.navigateTo();
  });

  it('should disable search when empty', () => {
    page.getSearchTextbox();
    expect(page.getSearchButton().isEnabled()).toBeFalsy();
  });

  it('should enable search when not empty', () => {
    page.getSearchTextbox().sendKeys('test');
    expect(page.getSearchButton().isEnabled()).toBeTruthy();
  });
});
