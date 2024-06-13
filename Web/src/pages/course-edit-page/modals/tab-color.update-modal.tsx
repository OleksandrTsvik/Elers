import { FormInstance, Modal } from 'antd';
import { useState } from 'react';
import { useTranslation } from 'react-i18next';

import { CourseEditModalMode } from './edit-modal-mode.enum';
import { useUpdateCourseTabColorMutation } from '../../../api/course-tabs.api';
import { CourseTab } from '../../../models/course-tab.interface';
import TabColorForm, { TabColorFormValues } from '../forms/tab-color.form';
import useCourseEditState from '../use-course-edit.state';

interface Props {
  courseTab: CourseTab;
}

export default function TabColorUpdateModal({ courseTab }: Props) {
  const { t } = useTranslation();
  const { modalMode, onCloseModal } = useCourseEditState();

  const [formInstance, setFormInstance] =
    useState<FormInstance<TabColorFormValues>>();

  const [updateCourseTabColor, { isLoading, error }] =
    useUpdateCourseTabColorMutation();

  const handleSubmit = async ({ color }: TabColorFormValues) => {
    await updateCourseTabColor({ tabId: courseTab.id, color })
      .unwrap()
      .then(() => onCloseModal());
  };

  return (
    <Modal
      destroyOnClose
      open={modalMode === CourseEditModalMode.EditTabColor}
      confirmLoading={isLoading}
      title={t('course_edit_page.change_tab_color')}
      okText={t('actions.save_changes')}
      onOk={formInstance?.submit}
      onCancel={onCloseModal}
    >
      <TabColorForm
        initialValues={{ color: courseTab.color }}
        tabName={courseTab.name}
        error={error}
        onSubmit={handleSubmit}
        onFormInstanceReady={setFormInstance}
      />
    </Modal>
  );
}
