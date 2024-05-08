import { Button, FormInstance, Modal } from 'antd';
import { useState } from 'react';
import { useTranslation } from 'react-i18next';

import CourseCreationForm, {
  CourseCreationFormValues,
} from './course-creation.form';
import { useCreateCourseMutation } from '../../api/courses.api';
import useModal from '../../hooks/use-modal';

export default function CourseCreationModalButton() {
  const { t } = useTranslation();
  const { isOpen, onOpen, onClose } = useModal();

  const [formInstance, setFormInstance] =
    useState<FormInstance<CourseCreationFormValues>>();

  const [createCourse, { isLoading, error }] = useCreateCourseMutation();

  const handleSubmit = async (values: CourseCreationFormValues) => {
    await createCourse(values)
      .unwrap()
      .then(() => onClose());
  };

  return (
    <>
      <Button type="primary" onClick={onOpen}>
        {t('my_courses_page.create_course')}
      </Button>
      <Modal
        destroyOnClose
        open={isOpen}
        confirmLoading={isLoading}
        title={t('my_courses_page.creation_title')}
        okText={t('actions.add')}
        onOk={formInstance?.submit}
        onCancel={onClose}
      >
        <CourseCreationForm
          error={error}
          onFormInstanceReady={setFormInstance}
          onSubmit={handleSubmit}
        />
      </Modal>
    </>
  );
}
