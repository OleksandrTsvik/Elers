import { CourseTabType } from './course-tab.enum';
import { SEARCH_PARAM_COURSE_TAB } from './course-tabs.constants';
import { CourseTab } from '../../models/course-tab.interface';
import {
  deleteQueryParam,
  getQueryParam,
  updateQueryParam,
} from '../../utils/helpers';

export function getCourseTabFromQueryParamOrFirst(tabs: CourseTab[]) {
  const tabId = getQueryParam(SEARCH_PARAM_COURSE_TAB);

  if (tabId && tabs.some((item) => item.id === tabId)) {
    return tabId;
  }

  if (tabs.length) {
    return tabs[0].id;
  }
}

export function setCourseTabToQueryParam(tabId: string) {
  updateQueryParam(SEARCH_PARAM_COURSE_TAB, tabId);
}

export function setCourseTabToQueryParamByType(
  tabType: CourseTabType,
  tabId: string,
) {
  if (tabType === CourseTabType.Tabs) {
    setCourseTabToQueryParam(tabId);
  }
}

export function deleteCourseTabFromQueryParam() {
  deleteQueryParam(SEARCH_PARAM_COURSE_TAB);
}
