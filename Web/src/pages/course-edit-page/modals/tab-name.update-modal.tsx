import { useTranslation } from 'react-i18next';

import { CourseEditModalMode } from './edit-modal-mode.enum';
import TabNameModal from './tab-name.modal';
import { useUpdateCourseTabMutation } from '../../../api/course-tabs.api';
import { CourseTab } from '../../../models/course-tab.interface';
import { TabNameFormValues } from '../forms/tab-name.form';
import useCourseEditState from '../use-course-edit.state';

interface Props {
  courseTab: CourseTab;
}

export default function TabNameUpdateModal({ courseTab }: Props) {
  const { t } = useTranslation();
  const { modalMode, onCloseModal } = useCourseEditState();

  const [updateCourseTab, { isLoading, error }] = useUpdateCourseTabMutation();

  const handleSubmit = async ({ tabName }: TabNameFormValues) => {
    await updateCourseTab({
      tabId: courseTab.id,
      name: tabName,
    }).unwrap();
  };

  return (
    <TabNameModal
      isOpen={modalMode === CourseEditModalMode.EditTabName}
      initialValues={{ tabName: courseTab.name }}
      textTitle={t('course_edit_page.change_tab_name')}
      textOnSubmitButton={t('actions.save_changes')}
      isLoading={isLoading}
      error={error}
      onClose={onCloseModal}
      onSubmit={handleSubmit}
    />
  );
}
