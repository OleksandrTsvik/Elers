import { CourseTab } from '../../../models/course-tab.interface';
import { CourseTabType, SEARCH_PARAM_COURSE_TAB } from '../../../shared';
import {
  deleteQueryParam,
  getQueryParam,
  updateQueryParam,
} from '../../../utils/helpers';

export default function useTabQueryParams() {
  const getTabFromQueryOrFirst = (tabs: CourseTab[]) => {
    const tabId = getQueryParam(SEARCH_PARAM_COURSE_TAB);

    if (tabId && tabs.some((item) => item.id === tabId)) {
      return tabId;
    }

    if (tabs.length) {
      return tabs[0].id;
    }
  };

  const setTab = (tabId: string) => {
    updateQueryParam(SEARCH_PARAM_COURSE_TAB, tabId);
  };

  const setTabByType = (tabType: string | undefined, tabId: string) => {
    if (!tabType || tabType === CourseTabType.Tabs.toString()) {
      setTab(tabId);
    }
  };

  const deleteTab = () => {
    deleteQueryParam(SEARCH_PARAM_COURSE_TAB);
  };

  return { getTabFromQueryOrFirst, setTab, setTabByType, deleteTab };
}
