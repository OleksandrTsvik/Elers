import { useEffect, useState } from 'react';
import {
  DragDropContext,
  Draggable,
  DropResult,
  Droppable,
} from 'react-beautiful-dnd';

import TabContentByType from './tab.content-by-type';
import TabContentEditPanel from './tab.content-edit-panel';
import { useReorderCourseMaterialsMutation } from '../../../api/course-materials.mutations.api';
import useDisplayError from '../../../hooks/use-display-error';
import { CourseMaterial } from '../../../models/course-material.type';
import { classnames, handleDropResult } from '../../../utils/helpers';
import MaterialCreationButton from '../material-creation/material-creation.button';

interface Props {
  tabId: string;
  materials: CourseMaterial[];
}

export default function TabContent({ tabId, materials }: Props) {
  const [reorderedMaterials, setReorderedMaterials] = useState(materials);

  const { displayError } = useDisplayError();
  const [reorderCourseMaterials] = useReorderCourseMaterialsMutation();

  useEffect(() => {
    setReorderedMaterials(materials);
  }, [materials]);

  const handleDragEnd = async (result: DropResult) => {
    const reorderedData = handleDropResult(result, reorderedMaterials);

    if (reorderedData) {
      setReorderedMaterials(reorderedData);

      const reorders = reorderedData.map(({ id }, index) => ({
        id,
        order: index,
      }));

      await reorderCourseMaterials({ reorders })
        .unwrap()
        .catch((error) => displayError(error));
    }
  };

  return (
    <>
      <DragDropContext onDragEnd={handleDragEnd}>
        <Droppable droppableId="courseTabMaterials">
          {(provided) => (
            <div ref={provided.innerRef} {...provided.droppableProps}>
              {reorderedMaterials.map((item, index) => (
                <Draggable
                  key={item.id}
                  draggableId={`courseTabMaterials-${item.id}`}
                  index={index}
                >
                  {(provided) => (
                    <div
                      className={classnames('d-block', 'mt-course_material')}
                      ref={provided.innerRef}
                      {...provided.draggableProps}
                    >
                      <TabContentEditPanel
                        material={item}
                        dragHandleProps={provided.dragHandleProps}
                      />
                      <TabContentByType material={item} />
                    </div>
                  )}
                </Draggable>
              ))}
              {provided.placeholder}
            </div>
          )}
        </Droppable>
      </DragDropContext>

      <MaterialCreationButton tabId={tabId} />
    </>
  );
}
