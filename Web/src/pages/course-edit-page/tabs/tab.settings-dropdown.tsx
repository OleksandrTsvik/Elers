import useTabActions from './use-tab.actions';
import { SettingsDropdown } from '../../../components';
import { CourseTab } from '../../../models/course-tab.interface';

interface Props {
  courseTab: CourseTab;
}

export default function TabSettingsDropdown({ courseTab }: Props) {
  const { tabActions, isLoading } = useTabActions(courseTab);

  return <SettingsDropdown items={tabActions} loading={isLoading} />;
}
