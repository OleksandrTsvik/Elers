import { DragOutlined } from '@ant-design/icons';
import { Button, List, Modal } from 'antd';
import { useEffect, useState } from 'react';
import {
  DragDropContext,
  Draggable,
  DropResult,
  Droppable,
} from 'react-beautiful-dnd';
import { useTranslation } from 'react-i18next';

import { useReorderCourseTabsMutation } from '../../api/course-tabs.api';
import { ErrorAlert } from '../../common/error';
import useModal from '../../hooks/use-modal';
import { CourseTab } from '../../models/course-tab.interface';
import { handleDropResult } from '../../utils/helpers';

import styles from './course-edit.module.scss';

interface Props {
  courseTabs: CourseTab[];
}

export default function ReorderTabsModalButton({ courseTabs }: Props) {
  const { t } = useTranslation();

  const [reorderedTabs, setReorderedTabs] = useState(courseTabs);
  const { isOpen, onOpen, onClose } = useModal();

  const [reorderCourseTabs, { isLoading, error }] =
    useReorderCourseTabsMutation();

  useEffect(() => {
    setReorderedTabs(courseTabs);
  }, [courseTabs]);

  const handleDragEnd = (result: DropResult) => {
    const reorderedData = handleDropResult(result, reorderedTabs);

    if (reorderedData) {
      setReorderedTabs(reorderedData);
    }
  };

  const handleSubmit = async () => {
    const reorders = reorderedTabs.map(({ id }, index) => ({
      id,
      order: index,
    }));

    await reorderCourseTabs({ reorders })
      .unwrap()
      .then(() => onClose());
  };

  return (
    <>
      <Button className="right-btn" onClick={onOpen}>
        {t('course_edit_page.reorder_tabs')}
      </Button>
      <Modal
        open={isOpen}
        confirmLoading={isLoading}
        title={t('course_edit_page.reorder_tabs')}
        okText={t('actions.save_changes')}
        onOk={handleSubmit}
        onCancel={onClose}
      >
        <DragDropContext onDragEnd={handleDragEnd}>
          <Droppable droppableId="courseTabs">
            {(provided) => (
              <div ref={provided.innerRef} {...provided.droppableProps}>
                <ErrorAlert error={error} />
                <List
                  dataSource={reorderedTabs}
                  rowKey={(tab) => tab.id}
                  renderItem={({ id, name, color }, index) => (
                    <Draggable draggableId={`courseTabs-${id}`} index={index}>
                      {(provided) => (
                        <List.Item
                          ref={provided.innerRef}
                          {...provided.draggableProps}
                        >
                          <DragOutlined
                            className={styles.gradTabIcon}
                            {...provided.dragHandleProps}
                          />
                          {index + 1}. <span style={{ color }}>{name}</span>
                        </List.Item>
                      )}
                    </Draggable>
                  )}
                />
                {provided.placeholder}
              </div>
            )}
          </Droppable>
        </DragDropContext>
      </Modal>
    </>
  );
}
