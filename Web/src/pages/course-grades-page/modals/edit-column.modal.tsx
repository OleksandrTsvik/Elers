import { Button, FormInstance, Modal, Popconfirm } from 'antd';
import { useState } from 'react';
import { useTranslation } from 'react-i18next';

import { GradesModalMode } from './grades-modal-mode.enum';
import {
  useDeleteManualGradesColumnMutation,
  useUpdateManualGradesColumnMutation,
} from '../../../api/grades-columns.api';
import useDisplayError from '../../../hooks/use-display-error';
import { GradeType } from '../../../models/grade.interface';
import GradesColumnForm, {
  GradesColumnFormValues,
} from '../forms/grades-column.form';
import useCourseGradesState from '../use-course-grades.state';

export default function EditColumnModal() {
  const { t } = useTranslation();
  const { modalMode, activeColumn, onCloseModal } = useCourseGradesState();

  const { displayError } = useDisplayError();

  const [formInstance, setFormInstance] =
    useState<FormInstance<GradesColumnFormValues>>();

  const [updateColumn, { isLoading, error }] =
    useUpdateManualGradesColumnMutation();

  const [deleteColumn] = useDeleteManualGradesColumnMutation();

  const handleSubmit = async (values: GradesColumnFormValues) => {
    if (!activeColumn) {
      return;
    }

    await updateColumn({ columnId: activeColumn.id, ...values })
      .unwrap()
      .then(() => onCloseModal());
  };

  const handleDelete = async () => {
    if (!activeColumn) {
      return;
    }

    await deleteColumn({ columnId: activeColumn.id })
      .unwrap()
      .then(() => onCloseModal())
      .catch((error) => displayError(error));
  };

  return (
    <Modal
      open={modalMode === GradesModalMode.EditManualColumn}
      title={t('course_grades_page.edit_column')}
      onCancel={onCloseModal}
      footer={[
        <Popconfirm
          key="delete"
          placement="bottom"
          title={t('actions.confirm_deletion')}
          description={t('actions.confirm_delete')}
          okText={t('actions.delete')}
          okButtonProps={{ danger: true }}
          onConfirm={handleDelete}
        >
          <Button danger type="dashed">
            {t('actions.delete')}
          </Button>
        </Popconfirm>,
        <Button key="cancel" onClick={onCloseModal}>
          {t('actions.cancel')}
        </Button>,
        <Button
          key="submit"
          type="primary"
          loading={isLoading}
          onClick={formInstance?.submit}
        >
          {t('actions.save_changes')}
        </Button>,
      ]}
    >
      <GradesColumnForm
        initialValues={
          activeColumn?.type === GradeType.Manual
            ? activeColumn
            : { title: '', date: new Date(), maxGrade: 10 }
        }
        error={error}
        onSubmit={handleSubmit}
        onFormInstanceReady={setFormInstance}
      />
    </Modal>
  );
}
