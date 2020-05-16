import { browser, by, element } from 'protractor';

export class HomePage {
  navigateTo() {
    return browser.get('/');
  }

  getSearchTextbox() {
    return element(by.name('term'));
  }

  getSearchButton() {
    return element(by.buttonText('Search'));
  }
}
