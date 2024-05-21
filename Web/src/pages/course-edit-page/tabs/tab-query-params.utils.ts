import { CourseTab } from '../../../models/course-tab.interface';
import { CourseTabType, SEARCH_PARAM_COURSE_TAB } from '../../../shared';
import {
  deleteQueryParam,
  getQueryParam,
  updateQueryParam,
} from '../../../utils/helpers';

export function getTabFromQueryOrFirst(tabs: CourseTab[]) {
  const tabId = getQueryParam(SEARCH_PARAM_COURSE_TAB);

  if (tabId && tabs.some((item) => item.id === tabId)) {
    return tabId;
  }

  if (tabs.length) {
    return tabs[0].id;
  }
}

export function setTab(tabId: string) {
  updateQueryParam(SEARCH_PARAM_COURSE_TAB, tabId);
}

export function setTabByType(tabType: CourseTabType, tabId: string) {
  if (tabType === CourseTabType.Tabs) {
    setTab(tabId);
  }
}

export function deleteTab() {
  deleteQueryParam(SEARCH_PARAM_COURSE_TAB);
}
