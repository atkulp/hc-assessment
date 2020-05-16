import { AppPage } from './app.po';

describe('Assessment App', () => {
  let page: AppPage;

  beforeEach(() => {
    page = new AppPage();
  });

  it('should navigate to Home', () => {
    page.navigateTo('/');
    expect(page.getTitleText()).toEqual('Home');
  });

  it('should navigate to About', () => {
    page.navigateTo("/About");
    expect(page.getTitleText()).toEqual('About');
  });
});
