import { Tabs } from 'antd';
import { useSearchParams } from 'react-router-dom';

import { getTabsItems } from './tab.utils';
import { useAppDispatch } from '../../../hooks/redux-hooks';
import { CourseTab } from '../../../models/course-tab.interface';
import { SEARCH_PARAM_COURSE_TAB } from '../../../shared';
import { setModalMode } from '../course-edit.slice';
import { CourseTabModalMode } from '../modals/tab-modal-mode.enum';

interface Props {
  tabs: CourseTab[];
}

export default function TabListContent({ tabs }: Props) {
  const [searchParams, setSearchParams] = useSearchParams({
    [SEARCH_PARAM_COURSE_TAB]: tabs[0]?.id,
  });

  const appDispatch = useAppDispatch();

  const items = getTabsItems(tabs);

  const onChange = (activeKey: string) => {
    setSearchParams((prev) => ({
      ...prev,
      [SEARCH_PARAM_COURSE_TAB]: activeKey,
    }));
  };

  const onEdit = (action: 'add' | 'remove') => {
    if (action === 'add') {
      appDispatch(setModalMode(CourseTabModalMode.CreateTab));
    }
  };

  return (
    <Tabs
      destroyInactiveTabPane
      type="editable-card"
      activeKey={searchParams.get('tab') ?? undefined}
      items={items}
      onChange={onChange}
      onEdit={(_, action) => onEdit(action)}
    />
  );
}
