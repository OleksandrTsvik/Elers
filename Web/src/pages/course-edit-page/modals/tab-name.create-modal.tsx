import { useTranslation } from 'react-i18next';

import { CourseEditModalMode } from './edit-modal-mode.enum';
import TabNameModal from './tab-name.modal';
import { useCreateCourseTabMutation } from '../../../api/course-tabs.api';
import { CourseTabType, setCourseTabToQueryParamByType } from '../../../shared';
import { TabNameFormValues } from '../forms/tab-name.form';
import useCourseEditState from '../use-course-edit.state';

interface Props {
  courseId: string;
  tabType: CourseTabType;
}

export default function TabNameCreateModal({ courseId, tabType }: Props) {
  const { t } = useTranslation();

  const { modalMode, onCloseModal } = useCourseEditState();
  const [createCourseTab, { isLoading, error }] = useCreateCourseTabMutation();

  const handleSubmit = async ({ tabName }: TabNameFormValues) => {
    await createCourseTab({ courseId, name: tabName })
      .unwrap()
      .then((tabId) => setCourseTabToQueryParamByType(tabType, tabId));
  };

  return (
    <TabNameModal
      isOpen={modalMode === CourseEditModalMode.CreateTab}
      initialValues={{ tabName: '' }}
      textTitle={t('course_edit_page.new_section')}
      textOnSubmitButton={t('course_edit_page.add_section')}
      isLoading={isLoading}
      error={error}
      okButtonProps={{ className: 'btn-success' }}
      onClose={onCloseModal}
      onSubmit={handleSubmit}
    />
  );
}
