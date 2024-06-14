import { PlusOutlined } from '@ant-design/icons';
import { Button, FormInstance, Modal } from 'antd';
import { useState } from 'react';
import { useTranslation } from 'react-i18next';

import { useCreateManualGradesColumnMutation } from '../../../api/grades-columns.api';
import useModal from '../../../hooks/use-modal';
import GradesColumnForm, {
  GradesColumnFormValues,
} from '../forms/grades-column.form';

interface Props {
  courseId: string | undefined;
}

export default function AddColumnModalButton({ courseId }: Props) {
  const { t } = useTranslation();
  const { isOpen, onOpen, onClose } = useModal();

  const [formInstance, setFormInstance] =
    useState<FormInstance<GradesColumnFormValues>>();

  const [createColumn, { isLoading, error }] =
    useCreateManualGradesColumnMutation();

  const handleSubmit = async (values: GradesColumnFormValues) => {
    await createColumn({ courseId, ...values })
      .unwrap()
      .then(() => onClose());
  };

  return (
    <>
      <Button
        className="right-btn mb-field"
        type="primary"
        icon={<PlusOutlined />}
        onClick={onOpen}
      >
        {t('course_grades_page.add_column')}
      </Button>

      <Modal
        open={isOpen}
        confirmLoading={isLoading}
        title={t('course_grades_page.add_column')}
        okText={t('actions.add')}
        onOk={formInstance?.submit}
        onCancel={onClose}
      >
        <GradesColumnForm
          initialValues={{ title: '', date: new Date(), maxGrade: 10 }}
          error={error}
          onSubmit={handleSubmit}
          onFormInstanceReady={setFormInstance}
        />
      </Modal>
    </>
  );
}
